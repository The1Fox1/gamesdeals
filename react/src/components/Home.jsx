import React, { useState } from "react";
import { withRouter } from "react-router-dom";
import { connect } from "react-redux";
import { getGame, addGame, addGameList } from "../actions/xActions";
import {
  AppBar,
  Box,
  Button,
  Card,
  CardContent,
  CardMedia,
  Container,
  Grid,
  Menu,
  MenuItem,
  MenuList,
  Paper,
  Slide,
  TextField,
  Toolbar,
  Typography,
  useScrollTrigger
} from "@material-ui/core";
import { Modal } from "reactstrap";
import { makeStyles, withStyles } from "@material-ui/core/styles";
import * as scraperServices from "../services/scraperServices";
import * as ITADServices from "../services/ITADServices";
import { toast } from "react-toastify";

class Home extends React.Component {
  state = {
    searchModalIsOpen: false,
    search: "",
    searchedGame: {}
  };
  componentWillMount = () => {
    scraperServices
      .getTop20()
      .then(this.scrape20Success)
      .catch(this.scrape20Fail);
  };
  scrape20Success = data => {
    this.props.addGameList(data.items);
  };
  scrape20Fail = xhr => {
    console.log("get Top 20 Failed: ", xhr);
    //this.props.getGame("witcheriiassassinsofkingsenhancededition");
  };

  mapGamesToDom = (game, indx) => {
    return (
      <Grid
        id={game.appId ? "app/" + game.appId : "app/unkown-" + indx}
        item
        xs={4}
        key={game.appId ? "Game_A_" + game.appId : "Game_I_" + indx}
      >
        <Card>
          <CardContent>
            <CardMedia
              className="py-5"
              image={game.image}
              title="Image title"
            />
            <Typography gutterBottom variant="h5" component="h2">
              <a href={game.url}>{game.title}</a>
            </Typography>
            <Typography>
              Retails: {game.retailPrice} <br /> Sale Price:{" "}
              {game.salePrice !== 0 ? game.salePrice : "---"}
            </Typography>
            <Button color="primary" variant="contained">
              Save Game
            </Button>
          </CardContent>
        </Card>
      </Grid>
    );
  };

  searchHandler = e => {
    const input = e.target.value;
    this.setState({ search: input });
  };
  submitSearch = e => {
    e.preventDefault();
    ITADServices.search(this.state.search)
      .then(this.searchSuccess)
      .catch(this.searchError);
  };
  searchSuccess = data => {
    debugger;
    this.setState(state => {
      return {
        ...state,
        searchedGame: data.data,
        searchedJsx: this.returnJsx(data.data.list),
        searchModalIsOpen: true
      };
    });
  };
  returnJsx = (arr, indx) => {
    const game = arr[0];
    return (
      <Grid item key={game.appId ? "Game_A_" + game.appId : "Game_I_" + indx}>
        <Card>
          <CardContent>
            <Typography gutterBottom variant="h5" component="h2">
              <a href={game.urls.buy}>{game.title}</a>
            </Typography>
            <Typography>
              Retails: {game.retailPrice} <br /> Sale Price:{" "}
              {game.salePrice !== 0 ? game.salePrice : "---"}
            </Typography>
            <Button
              color="primary"
              variant="contained"
              onClick={() => toast.success("Saved to")}
            >
              Save Game
            </Button>
          </CardContent>
        </Card>
      </Grid>
    );
  };
  searchError = xhr => {
    debugger;
    console.log(xhr);
  };

  HideOnScroll = props => {
    const { children, window } = props;
    // Note that you normally won't need to set the window ref as useScrollTrigger
    // will default to window.
    // This is only being set here because the demo is in an iframe.
    const trigger = useScrollTrigger({ target: window ? window() : undefined });

    return (
      <Slide appear={false} direction="down" in={!trigger}>
        {children}
      </Slide>
    );
  };

  useStyles = () => {
    return makeStyles(theme => ({
      root: {
        flexGrow: 1
      },
      cust: {
        backgroundColor: "linear-gradient(45deg, #2196F3 30%, #21CBF3 90%)"
      }
    }));
  };

  toggleModal = () => {
    this.setState(state => {
      return {
        ...state,
        searchModalIsOpen: !state.searchModalIsOpen
      };
    });
  };

  render() {
    const classes = this.useStyles();

    return (
      <div>
        <Box mt={5} m={10}>
          {/* <this.HideOnScroll {...this.props}>
            <AppBar
              variant="contained"
              color="default"
              anchorEl={this.anchorEl}
            > */}
          <Paper className={classes.cust}>
            <MenuList>
              <MenuItem>
                <TextField
                  variant="outlined"
                  value={this.state.search}
                  onChange={this.searchHandler}
                  type="text"
                />
              </MenuItem>
              <Button
                onClick={this.submitSearch}
                variant="contained"
                color="primary"
              >
                Find
              </Button>
            </MenuList>
          </Paper>
          {/* </AppBar>
          </this.HideOnScroll> */}

          <h3 className="mr-5">Steam Top Sellers</h3>
          <Box px={3} borderRadius={16} width="65%" bgcolor="#6495ED" mx="auto">
            <Grid container spacing={2}>
              {this.props.games
                ? this.props.games.items.map(this.mapGamesToDom)
                : this.props.games.item}
            </Grid>
          </Box>
        </Box>
        <Modal isOpen={this.state.searchModalIsOpen} toggle={this.toggleModal}>
          <Grid item xs={4} />
        </Modal>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    games: state.x
  };
};

const mapDispatchToProps = { getGame, addGame, addGameList };

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withRouter(Home));
