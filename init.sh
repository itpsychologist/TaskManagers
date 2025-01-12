#!/bin/bash

# ����������, �� � ����� � volume
if [ -z "$(ls -A /app/Data)" ]; then
    echo "Volume �������. ������� ��������� ���� �����..."
    # ������� ��������� ���� �����
    cp /app/InitialData/TaskManagers.db /app/Data/
    # ������������ �������� ����� �������
    chmod 644 /app/Data/TaskManagers.db
else
    echo "�������� ������� ��� � volume. ������������� ��."
fi

# ��������� ����������
dotnet TaskManagers.dll