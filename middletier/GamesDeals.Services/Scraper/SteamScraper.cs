using IronWebScraper;
using System;
using System.Collections.Generic;
using GamesDeals.Models.Domain;
using System.IO;

namespace GamesDeals.Services.Scraper
{

    public class AppSetting
    {
        public static string GetAppRoot() => @"C:\SF.Code\gamedeals\middletier\GamesDeals.Services\Scraper";
    }

    public class SteamScraper : WebScraper
    {
        /// <summary>
        /// Override this method initialize your web-scraper.
        /// Important tasks will be to Request at least one start url... and set allowed/banned domain or url patterns.
        public override void Init()
        {
            License.LicenseKey = "LicenseKey"; // Write License Key
            this.LoggingLevel = WebScraper.LogLevel.All; // All Events Are Logged
            this.WorkingDirectory = AppSetting.GetAppRoot() + @"\Output\";
            //EnableWebCache(new TimeSpan(1, 30, 30));
            this.Request("https://store.steampowered.com/search/?filter=topsellers", Parse);
        }
        /// <summary>
        /// Override this method to create the default Response handler for your web scraper.
        /// If you have multiple page types, you can add additional similar methods.

        /// <param name="response">The http Response object to parse</param>
        public override void Parse(Response response)
        {
            List<SteamGame> gameList = new List<SteamGame>();
            int count = 0;

            /// Loop on all anchor tags
            foreach (var gameRow in response.Css("#search_result_container > div > a"))
            {

                SteamGame game = new SteamGame();

                game.Url = gameRow.GetAttribute("href");
                game.AppId = gameRow.GetAttribute("data-ds-appid");
                game.Title = gameRow.Css("span.title")[0].TextContentClean;
                game.Image = gameRow.Css("div > img")[0].Attributes["src"];

                if (gameRow.Css("div.search_released")[0].InnerText != "")
                {
                    game.ReleaseDate = Convert.ToDateTime(gameRow.Css("div.search_released")[0].InnerText);
                }
                else
                {
                    game.ReleaseDate = null;
                }

                if (gameRow.Css(".search_price.discounted").Length != 0)
                { 
                   string[] combinedPrices = gameRow.Css("div.search_price.discounted")[0].InnerTextClean.Split("$");
                    game.RetailPrice = float.Parse(combinedPrices[1]);
                    game.SalePrice = float.Parse(combinedPrices[2]);
                }
                else if(gameRow.Css(".search_price").Length != 0)
                {
                    game.RetailPrice = float.Parse(gameRow.Css("div.search_price")[0].InnerTextClean.TrimStart('$'));
                }
                    

                gameList.Add(game);
                //count++;
                //if(count == response.Css("#search_result_container > div > a").Length -1)
                //{
                //    Scrape(gameList, "SteamScraper.Jsonl");
                //}
                
            }

            //File.WriteAllText(@"Output/SteamScraper.Jsonl", gameList);
            Scrape(gameList, "SteamScraper.Jsonl");

        }
    }
}
