using GamesDeals.Models.Domain;
using GamesDeals.Models.Requests;
using GamesDeals.Services.Interfaces;
using Sabio.Data;
using Sabio.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace GamesDeals.Services.Services
{
    class GameServices : IGameServices
    {
        private IDataProvider _dataProvider;

        public GameServices(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public int Insert(GameAddRequest model)
        {
            int id = 0;

            _dataProvider.ExecuteNonQuery(
                "dbo.Games_Insert",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    SqlParameter param = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    paramCol.Add(param);

                    paramCol.AddWithValue("@Title", model.Title);
                    paramCol.AddWithValue("@Plain", model.Plain);
                    paramCol.AddWithValue("@CurrentLow", model.SalePrice);
                    paramCol.AddWithValue("@ReleaseDate", model.ReleaseDate);
                    paramCol.AddWithValue("@RetailPrice", model.RetailPrice);
                },
                returnParameters: delegate (SqlParameterCollection parameterCollection)
                {
                    System.Int32.TryParse(parameterCollection["@Id"].Value.ToString(), out id);
                }
             );
            return id;
        }


        public GameWithLinks GetById(int Id)
        {
            GameWithLinks model = null;

            _dataProvider.ExecuteCmd(
                "dbo.Games_SelectById_InnerJoinLinks",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@GameId", Id);
                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    model = new GameWithLinks();
                    int index = 0;

                    model.Id = reader.GetSafeInt32(index++);
                    model.Title = reader.GetSafeString(index++);
                    model.Plain = reader.GetSafeString(index++);
                    index++;//Column Sale Price is repeat of Current Low. Refactor when i reorganize tables
                    model.SalePrice = reader.GetSafeFloat(index++); //column currentLow
                    model.LastUpdated = reader.GetSafeDateTime(index++);
                    model.RetailPrice = reader.GetSafeFloat(index++);
                    model.ReleaseDate = reader.GetSafeDateTime(index++);

                    model.Links = new GameLinks();
                    model.Links.AppId = reader.GetSafeString(index++);
                    model.Links.Shop = reader.GetSafeString(index++);
                    model.Links.Url = reader.GetSafeString(index++);

                    
                }
              );
            return model;
        }

        public void Update(GameUpdateRequest model)
        {
            _dataProvider.ExecuteNonQuery(
                "dbo.Games_Update",
                inputParamMapper: delegate (SqlParameterCollection paramCol)
                {
                    paramCol.AddWithValue("@Id", model.Id);
                    paramCol.AddWithValue("@Title", model.Title);
                    paramCol.AddWithValue("@Plain", model.Plain);
                    paramCol.AddWithValue("@CurrentLow", model.SalePrice);
                    paramCol.AddWithValue("@RetailPrice", model.RetailPrice);
                }
             );
        }

        public void Delete(int Id)
        {
            _dataProvider.ExecuteNonQuery(
                "dbo.Games_DeleteGameAndLinks",
                inputParamMapper: delegate (SqlParameterCollection parameterCollection)
                {
                    parameterCollection.AddWithValue("@Id", Id);
                }
            );
        }

    }
}
