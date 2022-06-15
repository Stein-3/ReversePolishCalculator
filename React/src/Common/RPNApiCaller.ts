const SERVER_URL = "https://localhost:7187/RPNCalculator";

export const pushFormula = async (formula: string) => {
  const request: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    mode: "cors",
    body: JSON.stringify({ formula }),
  };
  const result = await fetch(`${SERVER_URL}/FormulaPush`, request);
  if (!result.ok) {
    throw Error(await result.text());
  }
};

export const getAllCommand = async () => {
  const result = await fetch(`${SERVER_URL}/AllCommand`);
  if (!result.ok) {
    throw new Error("Fail to get command");
  }

  const body = await result.json();
  return body.commands as string[];
};

export const clearStack = async () => {
  const resultProcessor = async (response: Response) => {
    if (!response.ok) {
      throw Error(await response.text());
    }
  };
  return await commandExecution("clear", resultProcessor);
};

export const popFormula = async (): Promise<string> => {
  const resultProcessor = async (response: Response) => {
    if (!response.ok) {
      throw new Error(await response.text());
    }

    const body = await response.json();
    return body.result;
  };
  return await commandExecution("pop", resultProcessor);
};

export const executeFormula = async () => {
  const resultProcessor = async (response: Response) => {
    if (!response.ok) {
      throw new Error(await response.text());
    }
  };
  return await commandExecution("run", resultProcessor);
};

export const displayStack = async (): Promise<string[]> => {
  const resultProcessor = async (response: Response) => {
    if (!response.ok) {
      throw new Error(await response.text());
    }

    const body = await response.json();
    return (body.result as string).split(" ").reverse();
  };
  return await commandExecution("display", resultProcessor);
};

const commandExecution = async <T>(
  name: string,
  func: (response: Response) => Promise<T>
): Promise<T> => {
  const request: RequestInit = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    mode: "cors",
    body: JSON.stringify({ name }),
  };
  const result = await fetch(`${SERVER_URL}/CommandExecution`, request);
  return await func(result);
};
