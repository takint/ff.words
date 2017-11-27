import React from 'react';
import { Card, Button, Icon } from 'semantic-ui-react';

export default function EntryCard({entry, deleteEntry}){
    return (
        <Card>
            <Card.Content>
                <Card.Header>
                    <Icon name='newspaper'/>
                    {entry.title} by {entry.createdUser}
                </Card.Header>
                <Card.Description>
                    <p><Icon name='cubes'/> {entry.content}</p>
                    <p><Icon name='mail outline'/> {entry.id}</p>
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <div className="ui two buttons">
                    <Button basic color="green">Edit</Button>
                    <Button basic color="red">Delete</Button>
                </div>
            </Card.Content>
        </Card>
    );
}

EntryCard.propTypes = {
    //entry: React.propTypes.object.isRequired
};