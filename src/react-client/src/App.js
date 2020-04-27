import React from 'react';
import { Admin, Resource } from 'react-admin';
import Dashboard from './Dashboard/Dashboard';
import { CustomerList } from "./Customers/CustomerList";
import { CustomerCreate } from "./Customers/CustomerCreate";
import { CustomerEdit } from "./Customers/CustomerEdit";
import dataProvider from './dataProvider';
import authProvider from './authProvider';

const App = () => (
    <Admin title="Принт Центер" dashboard={Dashboard} authProvider={authProvider} dataProvider={dataProvider}>
      <Resource name="customers" list={CustomerList} create={CustomerCreate} edit={CustomerEdit}/>
    </Admin>
);
export default App;