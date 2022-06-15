import React from "react";

export interface PushColProps {
  onButtonClick: (e: React.SyntheticEvent) => void;
  onInputChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  content: string;
}
