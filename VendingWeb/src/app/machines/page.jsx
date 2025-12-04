"use client";

import { Button } from "@/components/button";
import { AddMachinesModal } from "@/components/machinesPage/addMachinesModal";
import { MachinesTable } from "@/components/machinesPage/table";
import { Modal } from "@/components/modal";
import { fetchApi } from "@/services/netService";
import { useEffect, useState } from "react";

export default function Page() {
  const [machines, setMachines] = useState([]);
  const [uploadFileModalOpen, setUploadFileModalOpen] = useState(false);

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

  const headers = [
    "Status",
    "Name",
    "Model",
    "Manufacturer",
    "Location",
    "WorkSince",
    "NextMaintenance",
  ];
  const columns = {
    Status: {
      name: "Статус",
      value: (machine) => machine.Status,
      stringValue: (machine) => {
        const statusName = {
          Operational: "Работает",
          Broken: "Сломан",
          InService: "На обслуживании",
        }[machine.Status];
        return statusName;
      },
    },
    Name: { name: "Название автомата", value: (machine) => machine.Name },
    Model: { name: "Модель", value: (machine) => machine.Model.Name },
    Manufacturer: {
      name: "Компания",
      value: (machine) => machine.Model.Manufacturer,
    },
    Location: {
      name: "Адрес / Место",
      value: (machine) => `${machine.Address} ${machine.Location}`,
    },
    WorkSince: {
      name: "В работе с",
      value: (machine) => machine.StartDate,
      stringValue: (machine) =>
        machine.StartDate
          ? new Date(machine.StartDate).toLocaleDateString()
          : "Не работает",
    },
    NextMaintenance: {
      name: "Следующее обслуживание",
      value: (machine) => machine.NextMaintenanceDate,
      stringValue: (machine) =>
        machine.NextMaintenanceDate
          ? new Date(machine.NextMaintenanceDate).toLocaleDateString()
          : "Не установлено",
    },
  };

  return (
    <div className="machinesPage">
      <div className="machinesTable">
        <div className="machinesPageActions">
          <Button
            className={"machinesPageAddButon"}
            text={"Добавить"}
            onClick={() => setUploadFileModalOpen(true)}
          />
          {uploadFileModalOpen && (
            <AddMachinesModal
              isOpen={true}
              onClose={() => setUploadFileModalOpen(false)}
            />
          )}
        </div>

        <MachinesTable
          columns={columns}
          headers={headers}
          machines={machines}
        />
      </div>
    </div>
  );
}
