import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Resource } from 'ngx-jsonapi';
import { ProjectsService, Project } from '../projects.service';

@Component({
    selector: 'project',
    templateUrl: './project.component.html'
})
export class ProjectComponent {
    public project: Project;

    public constructor(
        protected projectsService: ProjectsService,

        private route: ActivatedRoute
    ) {
        route.params.subscribe(({ id }) => {
            projectsService.get(id, { include: [], ttl: 100 }).subscribe(
                project => {
                    this.project = project;
                },
                error => console.error('Could not load project.', error)
            );
        });
    }

    public newProject() {
        let project = this.projectsService.new();
        project.attributes.description = prompt('New project description:', '');
        if (!project.attributes.description) {
            return;
        }

        project.save()
          .subscribe(success => {
            console.log('project saved', project.toObject());
          });
    }

    public updateProject() {
      this.project.attributes.description = prompt('Project description:', this.project.attributes.description);

        this.project.save().subscribe(success => {
          console.log('project saved', this.project.toObject());
        });
    }
}
