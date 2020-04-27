import React from 'react';
import { Create, SimpleForm, TextInput} from 'react-admin';

export const CustomerCreate = (props) => (
    <Create {...props}>
        <SimpleForm>
            <TextInput source="name" />
            <TextInput source="description"/>
        </SimpleForm>
    </Create>
);