#!/bin/sh

# O objetivo desse script é aguardar o banco de dados estar 
# disponível antes de  prosseguir com execução da aplicação.

# DB_HOST é um valor de variável de ambiente

set -e

echo "HOST: ${DB_CONNECTION__HOST}"

while ! mysql -h ${DB_CONNECTION__HOST} -u ${DB_CONNECTION__USER} -p${DB_CONNECTION__PASSWORD} ${DB_CONNECTION__DATABASE} -e ";" ; do
  >&2 echo "Database is unavailable - sleeping"
  sleep 1
done

>&2 echo "Database is up - executing command"

exec $@
# este ultimo comando faz com que demais comandos executados 
# como argumento sejam executados em seguida.