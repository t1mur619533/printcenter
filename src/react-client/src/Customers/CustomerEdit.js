import React from 'react';
import { Edit, SimpleForm, TextInput } from 'react-admin';

export const CustomerEdit = (props) => (
    <Edit {...props}>
        <SimpleForm>
            <TextInput disabled label="Id" source="id" />
            <TextInput source="name" />
            <TextInput multiline source="description"/>
        </SimpleForm>
    </Edit>
);