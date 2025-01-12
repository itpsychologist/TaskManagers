FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Встановлюємо інструменти EF
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Копіюємо файл проекту і відновлюємо залежності
COPY *.csproj ./
RUN dotnet restore

# Копіюємо всі файли та білдимо проект
COPY . ./
RUN dotnet build -c Release -o /build

# Публікуємо проект
RUN dotnet publish -c Release -o /app

# Створюємо фінальний образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Копіюємо опубліковані файли
COPY --from=build /app ./

# Створюємо необхідні директорії
RUN mkdir -p /app/Data && \
    mkdir -p /app/InitialData && \
    chmod 755 /app/Data && \
    chmod 755 /app/InitialData

# Копіюємо початкову базу даних
# Замініть "your_database.db" на реальну назву вашого файлу бази даних
COPY InitialData/TaskManagers.db /app/InitialData/

# Встановлюємо необхідні утиліти
RUN apt-get update && apt-get install -y dos2unix && rm -rf /var/lib/apt/lists/*

# Копіюємо скрипт ініціалізації
COPY init.sh ./
RUN chmod +x ./init.sh && \
    dos2unix ./init.sh

# Встановлюємо змінні середовища
ENV ASPNETCORE_URLS=http://0.0.0.0:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Відкриваємо порт
EXPOSE 80

# Запускаємо скрипт ініціалізації
ENTRYPOINT ["./init.sh"]