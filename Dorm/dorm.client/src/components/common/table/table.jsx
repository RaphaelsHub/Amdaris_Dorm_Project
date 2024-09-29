import React from 'react';
import './table.css'; // Подключаем стили для таблицы
import TableHeader from '../tableHeader/tableHeader';

export default function Table({ columns, data, onRowClick, userRole }) {
  return (
    <table className="table">
      <TableHeader columns={columns} />
      <tbody>
        {data.map((item) => (
          <tr key={item.id} onClick={() => onRowClick(item.id, userRole)}>
            {columns.map((column) => (
              <td key={column}>{item[column.toLowerCase()]}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
}
