FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# ������������ ����������� EF
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# ������� ���� ������� � ���������� ���������
COPY *.csproj ./
RUN dotnet restore

# ������� �� ����� �� ������ ������
COPY . ./
RUN dotnet build -c Release -o /build

# �������� ������
RUN dotnet publish -c Release -o /app

# ��������� ��������� �����
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# ������� ���������� �����
COPY --from=build /app ./

# ��������� �������� ��������
RUN mkdir -p /app/Data && \
    mkdir -p /app/InitialData && \
    chmod 755 /app/Data && \
    chmod 755 /app/InitialData

# ������� ��������� ���� �����
# ������ "your_database.db" �� ������� ����� ������ ����� ���� �����
COPY InitialData/TaskManagers.db /app/InitialData/

# ������������ �������� ������
RUN apt-get update && apt-get install -y dos2unix && rm -rf /var/lib/apt/lists/*

# ������� ������ �����������
COPY init.sh ./
RUN chmod +x ./init.sh && \
    dos2unix ./init.sh

# ������������ ���� ����������
ENV ASPNETCORE_URLS=http://0.0.0.0:80
ENV ASPNETCORE_ENVIRONMENT=Production

# ³�������� ����
EXPOSE 80

# ��������� ������ �����������
ENTRYPOINT ["./init.sh"]