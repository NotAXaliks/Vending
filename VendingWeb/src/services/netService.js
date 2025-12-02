export const BASE_URL = "http://localhost:40004/api"

export const fetchApi = async (endpoint, method, body) => {
  try {
    const response = await fetch(`${BASE_URL}${endpoint}`, {
      method,
      headers: { "Content-Type": "application/json" },
      body: body ? JSON.stringify(body) : undefined,
    });

    const jsonData = await response.json();

    if (!jsonData.IsSuccess) return { isSuccess: false, data: null, error: jsonData.error || jsonData }

    return { isSuccess: true, data: jsonData.Data, error: null };
  } catch {
    return { isSuccess: false, data: null, error: "Ошибка при получении данных." };
  }
}
