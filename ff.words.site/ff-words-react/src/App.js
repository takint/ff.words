import React, { Component } from 'react';
import { NavLink, Route } from 'react-router-dom';
import { Container } from 'semantic-ui-react';
import EntryListPage from './pages/entry-list-page';
import EntryFormPage from './pages/entry-form-page';

class App extends Component {
    render() {
        return (
            <Container>
                <div className="ui two item menu">
                  <NavLink className="item" activeClassName="active" exact to="/">
                    Entry List
                  </NavLink>
                  <NavLink className="item" activeClassName="active" exact to="/entry/new">
                    Add Entry
                  </NavLink>
                </div>
                <Route exact path="/" component={EntryListPage} />
                <Route exact path="/entry/new" component={EntryFormPage} />
                <Route exact path="/entry/edit/:id" component={EntryFormPage} />
            </Container>
        );
    }
}

export default App;
