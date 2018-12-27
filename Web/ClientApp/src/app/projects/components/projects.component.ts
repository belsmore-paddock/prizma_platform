import { Component } from '@angular/core';
import { DocumentCollection } from 'ngx-jsonapi';
import { ProjectsService, Project } from './../projects.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'project',
    templateUrl: './projects.component.html'
})
export class ProjectsComponent {
    public projects: DocumentCollection<Project>;

    public constructor(private route: ActivatedRoute, private projectsService: ProjectsService) {
        route.queryParams.subscribe(({ page }) => {
            projectsService
                .all({
                    sort: ['description'],
                    page: { number: page || 1 },
                    ttl: 3600
                })
                .subscribe(
                    projects => {
                        this.projects = projects;
                        console.info('success projects controller', projects, 'page', page || 1, projects.page.number);
                    },
                    error => console.error('Could not load projects :(', error)
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
}
