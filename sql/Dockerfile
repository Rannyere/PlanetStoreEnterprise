FROM mysql:8.0.22

MAINTAINER me

ENV MYSQL_DATABASE=PlanetStoreEnterpriseDB \
    MYSQL_ROOT_PASSWORD=123456

ADD create-db-mysql.sql /docker-entrypoint-initdb.d

EXPOSE 3306