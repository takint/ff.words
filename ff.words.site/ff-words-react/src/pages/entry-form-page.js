import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { SubmissionError } from 'redux-form';
import { connect } from 'react-redux';
import { newEntry, saveEntry } from '../actions/entry-actions';
import EntryForm from '../components/entry-form';

class EntryFormPage extends Component {

    state = {
        redirect: false
    };

    componentDidMount(){
        this.props.newEntry();
    }

    submit = (entry) => {
        return this.props.saveEntry(entry)
        .then(response => this.setState({ redirect: true }))
        .catch(err => {
            throw new SubmissionError(this.props.errors);
        });
    };

    render() {
        return (
            <div>
                {
                    this.state.redirect ? <Redirect to='/' /> :
                    <EntryForm entry={this.props.entry} loading={this.props.loading} onSubmit={this.submit} />
                }
            </div>
        );
    };
}

function mapStateToProps(state){
    return {
        entry: state.entryStore.entry,
        errors: state.entryStore.errors
    };
}

export default connect(mapStateToProps, {newEntry, saveEntry})(EntryFormPage);