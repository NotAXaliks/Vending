"use client";

import { UserContext } from "@/context";
import { useRouter } from "next/navigation";
import React, { useContext } from "react";
import { Header as AntdHeader } from "antd/es/layout/layout";
import { Menu } from "antd";

export function Header() {
  const router = useRouter();
  const { openedPage, setOpenPage } = useContext(UserContext);

  const pages = [
    { label: "Торговые аппараты", key: "/machines" },
    { label: "Календарь обслуживания", key: "/maintenanceCalendar" },
    { label: "График работ", key: "/workCalendar" },
  ];

  const onSelectPage = (selectedPageId) => {
    setOpenPage(selectedPageId);

    router.push(selectedPageId);
  };

  return (
    <AntdHeader>
      <Menu theme="dark" items={pages} mode="horizontal" selectedKeys={[openedPage]} onClick={(e) => onSelectPage(e.key)} />
    </AntdHeader>
  );
}
