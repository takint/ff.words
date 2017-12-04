import React, { Component } from 'react';
import { Form, Grid, Button } from 'semantic-ui-react';
import { Field, reduxForm } from 'redux-form';
import classnames from 'classnames';

class EntryForm extends Component{

    renderField = ({ input, label, type, meta: { touched, error } }) => (
        <Form.Field className={classnames({error: touched && error})}>
            <label>{label}</label>
            <input {...input} placeholder={label} type={type} />
            {touched && error && <span className="error">{error.message}</span>}
        </Form.Field>
    );

    componentWillReceiveProps = (nextProps) => {
        const { entry } = nextProps;
        console.log(entry);
        if(entry.id !== this.props.entry.id){
            this.props.initialize(entry);
        }
    };

    render(){
        const { handleSubmit, pristine, submitting, loading } = this.props;

        return (
            <Grid>
                <Grid.Column>
                    <h1 style={{marginTop:"1em"}}>{this.props.entry.id ? 'Edit Entry' : 'Add new Entry' }</h1>
                    <Form onSubmit={handleSubmit} loading={loading}>
                        <Form.Group widths='equal'>
                            <Field name='title' type='text' component={this.renderField} label='Entry Title' />
                        </Form.Group>
                        <Form.Group widths='equal'>
                            <Field name='content' type='text' component={this.renderField} label='Entry Content' />
                        </Form.Group>
                        <Button primary type='submit' disabled={pristine || submitting}>Save</Button>
                    </Form>
                </Grid.Column>
            </Grid>
        );
    };
}

const validate = (values) => {
    const errors = {title:{}};

    if(!values.title){
        errors.title = {
            message: 'Title is not empty'
        };
    }

    return errors;
};

export default reduxForm({form: 'entry', validate})(EntryForm);