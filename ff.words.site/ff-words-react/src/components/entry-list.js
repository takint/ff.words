import React from 'react';
import { Card } from 'semantic-ui-react';
import EntryCard from './entry-card';

export default function EntryList({entries}){
    const cards = () => {
        return entries.map(entry => {
            return (
                <EntryCard  key={entry.id} entry={entry} />
            );
        })
    };
    
    return (
        <Card.Group>
            { cards() }
        </Card.Group>
    );
}