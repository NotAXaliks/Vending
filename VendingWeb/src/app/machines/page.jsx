"use client";

import { AddMachinesModal } from "@/components/machinesPage/addMachinesModal";
import { MachinesTable } from "@/components/machinesPage/table";
import { Button, Flex } from "antd";
import { useState } from "react";

export default function Page() {
  const [uploadFileModalOpen, setUploadFileModalOpen] = useState(false);

  return (
    <div className="machinesPage">
      <Flex justify="flex-end">
        <Button
          type="primary"
          onClick={() => setUploadFileModalOpen(true)}
        >Добавить</Button>
        {uploadFileModalOpen && (
          <AddMachinesModal
            isOpen={true}
            onClose={() => setUploadFileModalOpen(false)}
          />
        )}
      </Flex>

      <MachinesTable />
    </div>
  );
}
