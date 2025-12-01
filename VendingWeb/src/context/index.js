"use client";

import { createContext, useState } from "react";

export const UserContext = createContext(null);

export default function UserState({ children }) {
  const [openedPage, setOpenPage] = useState();
  const [language, setLanguage] = useState("ru");

  return (
    <UserContext.Provider
      value={{
        openedPage,
        setOpenPage,
        language,
        setLanguage,
      }}
    >
      {children}
    </UserContext.Provider>
  );
}
