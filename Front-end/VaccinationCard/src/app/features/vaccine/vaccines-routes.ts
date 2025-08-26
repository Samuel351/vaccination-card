import { Routes } from "@angular/router";
import { ConsultVaccines } from "./pages/consult-vaccines/consult-vaccines";

export const vaccinesRoutes: Routes = [
    {path: '', pathMatch:'full', redirectTo: 'consultar-vacinas'},
    {path: 'consultar-vacinas', component: ConsultVaccines}
];