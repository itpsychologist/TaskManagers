#!/bin/bash

# Перевіряємо, чи є файли у volume
if [ -z "$(ls -A /app/Data)" ]; then
    echo "Volume порожній. Копіюємо початкову базу даних..."
    # Копіюємо початкову базу даних
    cp /app/InitialData/TaskManagers.db /app/Data/
    # Встановлюємо правильні права доступу
    chmod 644 /app/Data/TaskManagers.db
else
    echo "Знайдено існуючі дані у volume. Використовуємо їх."
fi

# Запускаємо застосунок
dotnet TaskManagers.dll