#!/bin/sh

# O objetivo desse script é aguardar o banco de dados estar 
# disponível antes de  prosseguir com execução da aplicação.

# DB_HOST é um valor de variável de ambiente

set -e

echo "HOST: ${DatabaseConnection__Host}"

while ! mysql -h ${DatabaseConnection__Host} -u ${DatabaseConnection__Username} -p${DatabaseConnection__Password} ${DatabaseConnection__Database} -e ";" ; do
  >&2 echo "Database is unavailable - sleeping"
  sleep 1
done

>&2 echo "Database is up - executing command"

exec $@
# este ultimo comando faz com que demais comandos executados 
# como argumento sejam executados em seguida.