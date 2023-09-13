import React from 'react';
import { connect } from 'react-redux';
import { getGraph } from '../actions/actions.js';

var tableStyle = {
	border: '1px solid black',
    borderCollapse: 'collapse'
};

var tableHeaderStyle = {
	padding: '5px'
};

// Query component, fetches data as component mounts, allows user to query
let Query = React.createClass({
  componentDidMount() {
    this.props.dispatch(getGraph("{goldberg(id: 2) {id, character, actor, role, traits}}"));
  },
  render() {
    let dispatch = this.props.dispatch;
    let fetchInProgress = String(this.props.store.get('fetching'));
    let queryText;
    let goldberg = this.props.store.get('data').toObject();
    return (
      <div>
        <p>Fetch in progress: {fetchInProgress}</p>
        <table style = {tableStyle}>
        	<tbody>
	        	<tr>
	        		<th style = {tableHeaderStyle}>Character</th>
	        		<th style = {tableHeaderStyle}>Actor</th>
	        		<th style = {tableHeaderStyle}>Role</th>
	        		<th style = {tableHeaderStyle}>Traits</th>
	        	</tr>
	        </tbody>
	        <tbody>
	        	<tr>
			        <td style = {tableHeaderStyle}>{ goldberg.character }</td>
			        <td style = {tableHeaderStyle}>{ goldberg.actor }</td>
			        <td style = {tableHeaderStyle}>{ goldberg.role }</td>
			        <td style = {tableHeaderStyle}>{ goldberg.traits }</td>
			    </tr>
			</tbody>
		</table>
        <input ref={node => {queryText = node}}></input>
        <button onClick={() => {dispatch(getGraph(queryText.value))}}>
          query
        </button>
      </div>
    )
  }
});

// hook up component to store and dispatch method
// by creating container component using connect()
const mapStateToProps = (state) => {
  return {
    store: state
  }
};

export const QueryContainer = connect(
  mapStateToProps
)(Query);