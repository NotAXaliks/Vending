import { useState } from "react";
import csvParser from "csv-parser";
import { Readable } from "stream";
import { fetchApi } from "@/services/netService";
import { message, Modal, Typography, Upload } from "antd";

const webStreamToNodeStream = (webStream) => {
  const reader = webStream.getReader();

  return new Readable({
    async read() {
      const { done, value } = await reader.read();

      if (done) this.push(null);
      else this.push(Buffer.from(value));
    },
  });
}

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

const validateLine = (data, line) => {
  if (!requiredColumns.every((column) => column in data))
    throw new Error(
      `Неверные заголовки. Необходимы: ${requiredColumns.join(", ")}`
    );

  const errors = [];

  const paymentTypes = data.paymentTypes?.split(",");
  if (!paymentTypes?.length) {
    errors.push({ line, error: "Необходим хотя бы один paymentTypes." });
    return { errors };
  }
  if (!paymentTypes.every(type => requiredPaymentTypes.includes(type))) {
    errors.push({ line, error: `Неверный paymentTypes. Ожидается: ${requiredPaymentTypes.join("/")}. Получено: ${paymentTypes.join("/")}` });
    return { errors };
  }

  const fullData = {
    Name: data.name,
    ModelId: data.modelId,
    Location: data.location,
    Address: data.address,
    SerialNumber: data.serialNumber,
    InventoryNumber: data.inventoryNumber,
    PaymentTypes: paymentTypes,
  };

  if (data.timezone) {
    if (!requiredTimezones.includes(data.timezone)) errors.push({ line, error: `Неверный timezone. Установлено значение по умолчанию.` })
    else fullData.Timezone = data.timezone;
  }

  if (data.priority) {
    if (!requiredPriorities.includes(data.priority)) errors.push({ line, error: `Неверный priority. Установлено значение по умолчанию.` })
    else fullData.Priority = data.priority;
  }

  return { data: fullData, errors };
}

function AddMachinesModal({ isOpen, onClose }) {
  const [uploadFormOpened, setUploadFormOpened] = useState(true);
  const [afterUploadMessage, setAfterUploadMessage] = useState("");

  const requestQueue = {
    index: 0,
    successCount: 0,
    queue: [],
    onEnd: () => { },
    add: async function (data) {
      this.queue.push(data);

      if (this.queue.length === 1) this.next();
    },
    next: async function () {
      if (!this.queue.length) return this.onEnd();

      const machineNum = ++this.index;

      setAfterUploadMessage(`Загрузка №${machineNum}...`);

      const machine = this.queue.shift();

      try {
        const response = await fetchApi("/machines", "PUT", machine);
        if (!response.isSuccess) {
          console.error(`Ошибка при загрузки аппарата №${machineNum}`, response.error);
          message.error(`Ошибка загрузки аппарата №${machineNum}`);
          return
        }

        this.successCount++;
      } finally {
        this.next();
      }
    }
  }

  const onFileUploaded = async (file) => {
    setUploadFormOpened(false);
    setAfterUploadMessage(`Загрузка...`);

    try {
      let lineNum = 0;
      webStreamToNodeStream(file.stream())
        .pipe(csvParser())
        .on("data", (rawData) => {
          try {
            const { data, errors } = validateLine(rawData, ++lineNum);
            if (!data) return console.error(errors);

            requestQueue.add(data);
          } catch (error) {
            setUploadFormOpened(true);
            console.error("Error uploading file:", error);
            return message.error("Ошибка при загрузке файла");
          }
        })
        .on("end", () => {
          requestQueue.onEnd = () => {
            onClose();
            if (requestQueue.successCount) {
              message.success(`Успешно загружено ${requestQueue.successCount} автоматов.`);
              setAfterUploadMessage(`Успешно загружено ${requestQueue.successCount} автоматов.`);
            }
          }
        });
    } catch (error) {
      setUploadFormOpened(true);
      console.error("Error uploading file:", error);
      return message.error("Ошибка при загрузке файла");
    }
  }

  const onFileChangeStatus = (info) => {
    if (info.file.status === "done") onFileUploaded(info.file.originFileObj)
  }

  const fileValidator = (file) => {
    if (!file.name.endsWith(".csv")) {
      message.error("Файл должен быть формата .csv");
      return Upload.LIST_IGNORE;
    }
    if (file.size > 1024 * 1024 * 1024) {
      message.error("Максимальный размер файла: 1ГБ");
      return Upload.LIST_IGNORE;
    }

    return true;
  }

  return (
    <Modal title="Добавить аппараты" centered open={isOpen} onCancel={onClose} footer={[]}>
      {uploadFormOpened ?
        <Upload.Dragger name="file" maxCount={1} accept=".csv" beforeUpload={fileValidator} onChange={onFileChangeStatus}>
          <p className="ant-upload-text">Нажмите сюда или перенесите нужный файл</p>
          <p className="ant-upload-hint">
            Файл должен быть формата .csv
          </p>
        </Upload.Dragger> : <Typography>{afterUploadMessage}</Typography>}
    </Modal>
  );
}

export { AddMachinesModal };
