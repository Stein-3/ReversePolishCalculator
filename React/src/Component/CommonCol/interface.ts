import React from "react";

export interface CommonColProps {
  label: string;
  buttonContent: string;
  onClickFunc: (e: React.SyntheticEvent) => void;
  result: string;
}
