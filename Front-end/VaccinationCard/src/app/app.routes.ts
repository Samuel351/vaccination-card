import { Routes } from '@angular/router';
import { Login } from './features/authentication/pages/login/login';
import { Home } from './features/home/pages/home/home';
import { personRoutes } from './features/person/person-routes';
import { vaccinesRoutes } from './features/vaccine/vaccines-routes';
import { AuthenticationGuard } from './features/authentication/guards/authentication-guard';
import { CreateUser } from './features/authentication/pages/create-user/create-user/create-user';

export const routes: Routes = [
    {path:'login', component: Login},
    {path:'criar-usuario', component: CreateUser},
    {path: '', component: Home, canActivate: [AuthenticationGuard], children: [
        {path: '', redirectTo: 'pessoas', pathMatch: 'full'},
        {path: 'pessoas', children: personRoutes},
        {path: 'vacinas', children: vaccinesRoutes}
]}
    
];
