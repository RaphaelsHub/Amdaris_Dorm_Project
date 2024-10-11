import React from 'react';
import './tableHeader.css'; // Подключаем стили для хедера

export default function TableHeader({ columns }) {
  return (
    <thead className="table-header">
      <tr>
        {columns.map((column) => (
          <th key={column}>{column}</th>
        ))}
      </tr>
    </thead>
  );
}
