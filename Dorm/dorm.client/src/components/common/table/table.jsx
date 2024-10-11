import React from 'react';
import './table.css'; 
import TableHeader from '../tableHeader/tableHeader';

export default function Table({ columns, data, onRowClick, userRole, respondentData }) {
  return (
    <table className="table">
      <TableHeader columns={columns} />
      <tbody>
        {data.map((item) => (
          <tr key={item.id} onClick={() => onRowClick(item.id, userRole, respondentData)}>
            {columns.map((column) => (
              <td key={column}>{item[column.toLowerCase()]}</td>
            ))}
          </tr>
        ))}
      </tbody>
    </table>
  );
}
