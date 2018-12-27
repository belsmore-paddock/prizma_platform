import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectComponent } from './components/project.component';
import { ProjectsComponent } from './components/projects.component';
import { ProjectsRoutingModule } from './projects-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [CommonModule, SharedModule, ProjectsRoutingModule],
    declarations: [ProjectComponent, ProjectsComponent]
})
export class ProjectsModule {}
