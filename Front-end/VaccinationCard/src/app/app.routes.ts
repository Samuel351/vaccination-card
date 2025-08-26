import { Routes } from '@angular/router';
import { ConsultPersons } from './features/person/pages/consult-persons/consult-persons';
import { PersonDetails } from './features/person/pages/person-details/person-details';
import { ConsultVaccines } from './features/vaccine/pages/consult-vaccines/consult-vaccines';

export const routes: Routes = [
    {path:'', pathMatch:'full', redirectTo: 'consultar-pessoas'},
    {path: 'consultar-pessoas', component: ConsultPersons},
    {path: 'consultar-vacinas', component: ConsultVaccines},
    { path: 'detalhes/:id', component: PersonDetails },
];
