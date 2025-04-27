FROM postgres:15.2-alpine3.17

EXPOSE 5432

RUN ls -l
COPY db-scripts/ /docker-entrypoint-initdb.d/

