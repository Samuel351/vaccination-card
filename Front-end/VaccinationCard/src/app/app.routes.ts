import { Routes } from '@angular/router';
import { ConsultPersons } from './features/person/pages/consult-persons/consult-persons';
import { PersonDetails } from './features/person/pages/person-details/person-details';

export const routes: Routes = [
    {path:'', pathMatch:'full', redirectTo: 'consult-consultar'},
    {path: 'consultar-pessoas', component: ConsultPersons},
    { path: 'detalhes/:id', component: PersonDetails }
];
