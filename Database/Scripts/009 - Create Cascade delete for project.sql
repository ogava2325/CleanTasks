ALTER TABLE Users_Projects
    ADD CONSTRAINT FK_UsersProjects_Projects
        FOREIGN KEY (ProjectId)
            REFERENCES Projects (Id)
            ON DELETE CASCADE;
