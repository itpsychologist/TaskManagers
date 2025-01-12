#!/bin/bash

# Проверяємо чи існує контейнер з іменем tender_shannon
if ! docker ps -a --format '{{.Names}}' | grep -q "^tender_shannon$"; then
    echo "Помилка: Контейнер tender_shannon не знайдено"
    exit 1
fi

# Перевіряємо поточний статус контейнера
CONTAINER_STATUS=$(docker inspect --format '{{.State.Status}}' tender_shannon)

# Якщо контейнер вже запущено, виводимо повідомлення
if [ "$CONTAINER_STATUS" = "running" ]; then
    echo "Контейнер tender_shannon вже запущено"
    exit 0
fi

# Запускаємо контейнер
echo "Запускаємо контейнер tender_shannon..."
docker start tender_shannon

# Перевіряємо чи успішно запущено контейнер
if [ $? -eq 0 ]; then
    echo "Контейнер tender_shannon успішно запущено"
else
    echo "Помилка при запуску контейнера tender_shannon"
    exit 1
fi