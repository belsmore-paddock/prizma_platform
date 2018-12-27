import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectsComponent } from './components/projects.component';
import { ProjectComponent } from './components/project.component';

export const routes: Routes = [
    {
        path: '',
        component: ProjectsComponent
    },
    {
        path: ':id',
        component: ProjectComponent
    }
];

@NgModule({
    imports: [ RouterModule.forChild(routes) ],
    exports: [ RouterModule ]
})
export class ProjectsRoutingModule {}
