import { Table } from "antd";
import { fetchApi } from "@/services/netService";
import { useEffect, useState } from "react";

function MachinesTable() {
  const [machines, setMachines] = useState([]);

  useEffect(() => {
    fetchApi("/machines", "POST", {}).then((data) => {
      if (!data.isSuccess)
        return console.error(
          "Ошибка при получении списка аппаратов",
          data.error
        );

      setMachines(data.data.Machines);
    });
  }, []);

  const statusRowData = {
    Operational: { name: "Работает", color: "green" },
    Broken: { name: "Сломан", color: "red" },
    InService: { name: "На обслуживании", color: "yellow" },
  };

  const columns = [
    {
      title: "Статус",
      dataIndex: "Status",
      filters: Object.entries(statusRowData).map(([id, { name }]) => ({
        text: name,
        value: id,
      })),
      onCell: (machine) => {
        return {
          style: {
            backgroundColor: statusRowData[machine.Status].color,
          },
        };
      },
      render: (status) => {
        return statusRowData[status].name;
      },
    },
    {
      title: "Название автомата",
      dataIndex: "Name",
      sorter: (a, b) => a.Name.localeCompare(b.name),
    },
    {
      title: "Модель",
      dataIndex: ["Model", "Name"],
      sorter: (a, b) => a.Model.Name.localeCompare(b.Model.Name),
    },
    {
      title: "Производитель",
      dataIndex: ["Model", "Manufacturer"],
      sorter: (a, b) =>
        a.Model.Manufacturer.localeCompare(b.Model.Manufacturer),
    },
    {
      title: "Адрес / Место",
      dataIndex: "Location",
      render: (_, machine) => `${machine.Address} ${machine.Location}`,
      sorter: (a, b) =>
        `${a.Address} ${a.Location}`.localeCompare(
          `${b.Address} ${b.Location}`
        ),
    },
    {
      title: "В работе с",
      dataIndex: "WorkSince",
      render: (_, machine) =>
        machine.StartDate
          ? new Date(machine.StartDate).toLocaleDateString()
          : "Не установлено",
      sorter: (a, b) => a.StartDate - b.StartDate,
    },
    {
      title: "Следующее обслуживание",
      dataIndex: "NextMaintenance",
      render: (_, machine) =>
        machine.NextMaintenanceDate
          ? new Date(machine.NextMaintenanceDate).toLocaleDateString()
          : "Не установлено",
      sorter: (a, b) => a.StartDate - b.StartDate,
    },
  ];

  return (
    <Table
      columns={columns}
      dataSource={machines}
      rowKey={(record) => record.Id}
    />
  );
}

export { MachinesTable };
