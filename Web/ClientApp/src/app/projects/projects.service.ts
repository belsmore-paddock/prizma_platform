import { Injectable } from '@angular/core';
import { Autoregister, Service, Resource, DocumentCollection, DocumentResource } from 'ngx-jsonapi';

export class Project extends Resource {
  public attributes = {
    description: ''
  };

  public relationships = {};
}

@Injectable()
@Autoregister()
export class ProjectsService extends Service<Project> {
  public resource = Project;
  public type = 'project';
}
