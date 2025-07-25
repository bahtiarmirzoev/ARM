"use client";
import React from "react";
import { useServiceRequests } from "@/hooks/use-api";
import { ServiceRequest } from "@/types";

export default function ServiceRequestsPage() {
  const { data, isLoading, error } = useServiceRequests();

  if (isLoading) return <div>Загрузка...</div>;
  if (error) return <div>Ошибка загрузки заявок</div>;

  return (
    <div>
      <h1>Заявки на сервис</h1>
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>Имя</th>
            <th>Телефон</th>
            <th>Марка</th>
            <th>Модель</th>
            <th>Описание</th>
            <th>Статус</th>
            <th>Дата</th>
          </tr>
        </thead>
        <tbody>
          {data?.data.map((req: ServiceRequest) => (
            <tr key={req.id}>
              <td>{req.id}</td>
              <td>{req.name}</td>
              <td>{req.phone}</td>
              <td>{req.make}</td>
              <td>{req.model}</td>
              <td>{req.problemDescription}</td>
              <td>{req.status}</td>
              <td>{new Date(req.createdAt).toLocaleString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
} 