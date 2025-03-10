Build started...
Build succeeded.
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE employees (
    id integer GENERATED BY DEFAULT AS IDENTITY,
    first_name text NOT NULL,
    last_name text NOT NULL,
    email text NOT NULL,
    document text NOT NULL,
    phones text[] NOT NULL,
    manager_id integer,
    password text NOT NULL,
    birthday timestamp with time zone NOT NULL,
    CONSTRAINT "PK_employees" PRIMARY KEY (id),
    CONSTRAINT "FK_employees_employees_manager_id" FOREIGN KEY (manager_id) REFERENCES employees (id) ON DELETE SET NULL
);

CREATE INDEX "IX_employees_manager_id" ON employees (manager_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250311013052_InitialCreate', '9.0.2');

ALTER TABLE employees ADD role_id integer NOT NULL DEFAULT 0;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250311033350_AddRoleToEmployee', '9.0.2');

INSERT INTO employees (first_name, last_name, email, document, phones, manager_id, password, birthday, role_id)
VALUES ('Super', 'User', 'admin@email.com', '0', ARRAY['0800-723-1111']::text[], NULL, 'pass@word1', TIMESTAMPTZ '1980-01-01T00:00:00Z', 1);

SELECT setval(
    pg_get_serial_sequence('employees', 'id'),
    GREATEST(
        (SELECT MAX(id) FROM employees) + 1,
        nextval(pg_get_serial_sequence('employees', 'id'))),
    false);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250311161702_AddDefaultUser', '9.0.2');

COMMIT;


