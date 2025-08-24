import { Routes } from '@angular/router';
import { ConsultPersons } from './features/person/pages/consult-persons/consult-persons';

export const routes: Routes = [
    {path:'', pathMatch:'full', redirectTo: 'consult-consultar'},
    {path: 'consultar-pessoas', component: ConsultPersons},
];
