import React from "react";
import { withRouter } from "react-router-dom";
import log from "debug";
import { connect } from "react-redux";
import { getGame, addGame, addList } from "../actions/xActions";
import Button from "@material-ui/core/Button";
import * as testServices from "../services/testServices";

class Home extends React.Component {
  state = {
    search: ""
  };
  componentWillMount = () => {
    testServices
      .getTop20()
      .then(this.testSuccess)
      .catch(this.testFail);
  };
  testSuccess = data => {
    debugger;
    this.props.addList(data.Items);
  };
  testFail = xhr => {
    debugger;
    console.log("get Top 20 Failed: ", xhr);
    this.props.getGame("witcheriiassassinsofkingsenhancededition");
  };
  searchHandler = e => {
    const input = e.target.value;
    this.setState({ search: input });
  };
  submitSearch = e => {
    e.preventDefault();

    this.props.addGame(this.state.search);
  };
  render() {
    return (
      <div>
        <input
          value={this.state.search}
          onChange={this.searchHandler}
          type="text"
        />
        <Button onClick={this.submitSearch}>Find</Button>
        <h3 className="mr-5">Select From these games</h3>
        <ul>
          {this.props.games.items.map((game, indx) => (
            <li key={indx}>{game.title}</li>
          ))}
        </ul>
        <h5>{this.props.games.item}</h5>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    games: state.x
  };
};

const mapDispatchToProps = { getGame, addGame, addList };

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withRouter(Home));
