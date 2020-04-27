import React from 'react';
import { Admin, Resource } from 'react-admin';

import Dashboard from './Dashboard';
import authProvider from './authProvider';
import {CustomerList} from "./customers";
import dataProvider from './dataProvider';

const App = () => (
    <Admin title="Принт Центер" dashboard={Dashboard} authProvider={authProvider} dataProvider={dataProvider}>
      <Resource name="customers" list={CustomerList} />
    </Admin>
);
export default App;