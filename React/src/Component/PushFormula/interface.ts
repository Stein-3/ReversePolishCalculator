import { CommonColProps } from "../CommonCol/interface";
import { PushColProps } from "../PushCol/interface";

export interface PushFormulaProps {
  push: PushColProps;
  pop: CommonCol;
  clear: CommonCol;
  run: CommonCol;
}

interface CommonCol {
  onClickFunc: (e: React.SyntheticEvent) => void;
  result: string;
}
