import { useState } from "react";
import csvParser from "csv-parser";
import { Modal } from "../modal";
import { Readable } from "stream";
import { fetchApi } from "@/services/netService";

function webStreamToNodeStream(webStream) {
  const reader = webStream.getReader();

  return new Readable({
    async read() {
      const { done, value } = await reader.read();

      if (done) this.push(null);
      else this.push(Buffer.from(value));
    },
  });
}

function UploadForm() {
  const [message, setMessage] = useState("");

  const requiredColumns = [
    "name",
    "modelId",
    "location",
    "address",
    "serialNumber",
    "inventoryNumber",
    "paymentTypes",
  ];
  const requiredPriorities = ["High", "Medium", "Low"];
  const requiredPaymentTypes = ["Coins", "Bill", "QR", "Card"];
  const requiredTimezones = [
    "UTC_12",
    "UTC_11",
    "UTC_10",
    "UTC_9",
    "UTC_8",
    "UTC_7",
    "UTC_6",
    "UTC_5",
    "UTC_4",
    "UTC_3",
    "UTC_2",
    "UTC_1",
    "UTC_0",
    "UTC1",
    "UTC2",
    "UTC3",
    "UTC4",
    "UTC5",
    "UTC6",
    "UTC7",
    "UTC8",
    "UTC9",
    "UTC10",
    "UTC12",
    "UTC12",
  ];

  const parseCsvFromStream = (stream) => {
    return new Promise((resolve, reject) => {
      const rows = [];

      stream
        .pipe(csvParser())
        .on("data", (data) => {
          if (!requiredColumns.every((column) => column in data))
            throw new Error(
              `Неверные заголовки. Необходимы: ${requiredColumns.join(", ")}`
            );

          const paymentTypes = requiredPaymentTypes.filter((type) =>
            data.paymentTypes.includes(type)
          );
          if (!paymentTypes.length) return;

          const fullData = {
            Name: data.name,
            ModelId: data.modelId,
            Location: data.location,
            Address: data.address,
            SerialNumber: data.serialNumber,
            InventoryNumber: data.inventoryNumber,
            PaymentTypes: paymentTypes,
          };

          if (data.timezone && requiredTimezones.includes(data.timezone)) {
            fullData.Timezone = data.timezone;
          }

          if (data.priority && requiredPriorities.includes(data.priority)) {
            fullData.Priority = data.priority;
          }

          rows.push(fullData);
        })
        .on("end", () => resolve(rows))
        .on("error", (error) => reject(error));
    });
  };

  const handleFileSelect = async (event) => {
    const [file] = event.target.files;
    if (!file) return;

    if (!file.name.endsWith(".csv"))
      return setMessage("файл должен быть формате .csv");

    if (file.size > 1024 * 1024 * 1024)
      return setMessage("Максимальный размер файла: 1ГБ");

    setMessage(`Загружаем... ${file.name}`);

    try {
      const machines = await parseCsvFromStream(
        webStreamToNodeStream(file.stream())
      );

      let successCount = 0;
      for (let i = 0; i < machines.length; i++) {
        const machine = machines[i];

        const data = await fetchApi("/machines", "PUT", {
          ...machine,
        });
        if (!data.isSuccess) {
          console.error(`Ошибка при загрузки аппарата №${i + 1}`, data.error);
          continue;
        }

        successCount++;

        setMessage(`Загрузка №${i + 1}...`);
      }

      return setMessage(`Успешно загружено ${machines.length} автоматов.`);
    } catch (error) {
      console.error("Error uploading file:", error);
      return setMessage("Ошибка при загрузке файла");
    }
  };

  return (
    <form className="machineFileUpload">
      <input
        id="file"
        type="file"
        name="file"
        className="fileInput"
        onChange={handleFileSelect}
      />
      <label htmlFor="file">
        <p>{message || "Загрузите файл .csv"}</p>
      </label>
    </form>
  );
}

function AddMachinesModal({ isOpen, onClose }) {
  const [] = useState(true);

  return (
    <Modal name="Добавить аппараты" open={isOpen} onClose={onClose}>
      <UploadForm />
    </Modal>
  );
}

export { AddMachinesModal };
