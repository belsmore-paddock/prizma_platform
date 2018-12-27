import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { ProjectsService } from '../projects.service';
import { ProjectsComponent } from './projects.component';
import { NgxJsonapiModule } from 'ngx-jsonapi';

describe('ProjectsComponent', () => {
    let component: ProjectsComponent;
    let fixture: ComponentFixture<ProjectsComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, NgxJsonapiModule],
            declarations: [ProjectsComponent],
            providers: [ProjectsService]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(ProjectsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
    it('projects should be filled', () => {
        expect(component.projects).toBeTruthy();
    });
});
