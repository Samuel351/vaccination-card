import { Routes } from "@angular/router";
import { ConsultPersons } from "./pages/consult-persons/consult-persons";
import { PersonDetails } from "./pages/person-details/person-details";

export const personRoutes: Routes = [
    {path: '', pathMatch:'full', redirectTo: 'consultar-pessoas'},
    {path:'consultar-pessoas', component: ConsultPersons},
    {path:'detalhes/:id', component: PersonDetails }
];