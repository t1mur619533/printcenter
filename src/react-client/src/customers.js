import React from 'react';
import { List, Datagrid, TextField } from 'react-admin';

export const CustomerList = props => (
    <List {...props}>
        <Datagrid rowClick="edit">
            <TextField source="name" />
            <TextField source="description" />
        </Datagrid>
    </List>
);