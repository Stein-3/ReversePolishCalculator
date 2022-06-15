import { atom } from "recoil";

export const StackState = atom<string[]>({
  key: "stackState",
  default: [],
});
