import React, { Component } from 'react';
import { connect } from 'react-redux';
import EntryList from '../components/entry-list';
import { fetchEntries } from '../actions/entry-actions';

class EntryListPage extends Component {

    componentDidMount(){
        this.props.fetchEntries();
    };

    render(){
        return (
            <div>
                <h1>List of Entries</h1>
                <EntryList entries={this.props.entries} />
            </div>
        );
    };
}

function mapStateToProps(state) {
    return {
        entries: state.entryStore.entries
    };
}

export default connect(mapStateToProps, {fetchEntries})(EntryListPage);