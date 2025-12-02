"use client";

import { fetchApi } from "@/services/netService";
import { useEffect, useRef, useState } from "react";

export default function Page() {
  const [machines, setMachines] = useState([]);
  const [sortByColumns, setSortByColumns] = useState([{ key: "Name", sort: "desc" }]);

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

  const formattedMachines = machines.map((machine) => {
    const object = { machine, formatted: {} };
    for (const headerId of headers) {
      const column = columns[headerId];
      object.formatted[headerId] = (column.stringValue || column.value)(machine);
    }
    return object;
  });

  let sortedMachines = formattedMachines;
  for (const { key, sort } of sortByColumns) {
    sortedMachines = sortedMachines.sort((a, b) => {
      const aValue = a.formatted[key];
      const bValue = b.formatted[key];

      if (sort === "desc") return bValue.localeCompare(aValue);
      if (sort === "asc") return aValue.localeCompare(bValue);

      return 0;
    });
  }

  const setSortBy = (headerId) => {
    console.log(headerId);
  }

  // const [message, setMessage] = useState("");

  // const uploadFile = async (formData) => {
  //   const file = formData.get("file");
  //   if (!file) return { error: "No file selected." };

  //   try {
  //     console.log(`Uploading file: ${file.name}, size: ${file.size} bytes`);

  //     const fileContent = Buffer.from(await file.arrayBuffer());
  //     console.log(fileContent.length);

  //     return { success: `File "${file.name}" uploaded successfully!` };
  //   } catch (error) {
  //     console.error("Error uploading file:", error);
  //     return { error: "Failed to upload file." };
  //   }
  // };

  // async function handleSubmit(event) {
  //   event.preventDefault();
  //   const formData = new FormData(event.currentTarget);
  //   const result = await uploadFile(formData);

  //   if (result.success) setMessage(result.success);
  //   else if (result.error) setMessage(result.error);
  // }

  return (
    <div className="machinesPage">
      <table className="machinesTable">
        <thead>
          <tr>
            {headers.map((headerId) => {
              const header = columns[headerId];
              return (
                <th onClick={() => setSortBy(headerId)}>{header.name}</th>
              );
            })}
          </tr>
        </thead>
        <tbody>
          {sortedMachines.map(({ machine, formatted }) => {
            const statusColor = {
              Operational: "green",
              Broken: "red",
              InService: "yellow",
            }[machine.Status];

            return (<tr key={machine.Id}>
              {headers.map((headerId) => {
                if (headerId === "Status") return (<th style={{ background: statusColor }}>{formatted.Status}</th>)

                return (<th>{formatted[headerId]}</th>)
              })}
            </tr>)
          })}
        </tbody>
      </table>
      {/* <div className="machineFileUploadContainer">
        <form onSubmit={handleSubmit} className="machineFileUpload">
          <input type="file" name="file" />
          <button type="submit">Upload</button>
          {message && <p>{message}</p>}
        </form>
      </div> */}
    </div>
  );
}
