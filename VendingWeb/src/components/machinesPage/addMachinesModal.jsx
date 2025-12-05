import { useState } from "react";
import csvParser from "csv-parser";
import { Modal } from "../modal";
import { Readable } from "stream";

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

function AddMachinesModal({ isOpen, onClose }) {
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
            name: data.name,
            modelId: data.modelId,
            location: data.location,
            address: data.address,
            serialNumber: data.serialNumber,
            inventoryNumber: data.inventoryNumber,
            paymentTypes,
          };

          if (data.timezone && requiredTimezones.includes(data.timezone)) {
            fullData.timezone = data.timezone;
          }

          if (data.priority && requiredPriorities.includes(data.priority)) {
            fullData.priority = data.priority;
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
      const data = await parseCsvFromStream(
        webStreamToNodeStream(file.stream())
      );

      return setMessage(`Успешно загружено ${data.length} автоматов.`);
    } catch (error) {
      console.error("Error uploading file:", error);
      return setMessage("Ошибка при загрузке файла");
    }
  };

  return (
    <Modal name="Добавить аппараты" open={isOpen} onClose={onClose}>
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
    </Modal>
  );
}

export { AddMachinesModal };
