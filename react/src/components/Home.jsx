import React from "react";
import { withRouter } from "react-router-dom";
import log from "debug";
import { connect } from "react-redux";
import { getGame, addGame } from "../actions/xActions";

class Home extends React.Component {
  state = {
    search: ""
  };
  componentWillMount = () => {
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
        <button onClick={this.submitSearch}>Find</button>
        <h3>Select From these games</h3>
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

const mapDispatchToProps = { getGame, addGame };

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(withRouter(Home));
